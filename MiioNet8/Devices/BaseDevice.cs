using MiioNet8.Commands;
using MiioNet8.Communication;
using MiioNet8.Interfaces;
using MiioNet8.Miot;
using MiioNet8.Protocol;
using MiioNet8.Responses;
using System.Net;
using System.Text.Json;

namespace MiioNet8.Devices
{
    public class BaseDevice : IDevice
    {
        public short Type { get; protected set; }

        public short Id { get; protected set; }

        public IToken Token { get; private set; }

        public int DeviceLastTimestamp { get; private set; }

        public DateTime DeviceLastCommunicationDateTime { get; private set; }
        public int DeviceLastCommandId { get; private set; }
        public virtual string Model { get; protected set; }

        private DeviceState State { get; set; }

        private ICommunication Communication { get; set; }

        protected Spec Spec { get; set; }

        public BaseDevice(ICommunication communication, IToken token)
        {
            Communication = communication;
            Token = token;
            State = DeviceState.Created;
        }

        public async Task<CommunicationResult> ConnectAsync()
        {
            var (result, answerPackage) = await Communicate(new HelloPackage(this));

            if (answerPackage != null)
            {
                Type = answerPackage.DeviceType;
                Id = answerPackage.DeviceId;

                if ((result = await UpdateMiioInfoAsync()) != CommunicationResult.Success)
                    return result;

                if ((result = await UpdateSpecAsync()) != CommunicationResult.Success)
                    return result;
            }

            State = DeviceState.Connected;

            return result;
        }

        public async Task<CommunicationResult> UpdateMiioInfoAsync()
        {
            var (result, response) = await SendCommandAsync<MiioInfoResponse>(new MiioInfoCommand());

            if (response != null)
            {
                if (string.IsNullOrEmpty(Model))
                    Model = response.Result.Model;
                else if (Model != response.Result.Model)
                    return CommunicationResult.Error;
            }

            return result;
        }

        public async Task<CommunicationResult> UpdateSpecAsync()
        {
            using var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://miot-spec.org/miot-spec-v2/instances?status=all");
            var instances = JsonSerializer.Deserialize<AllInstances>(json);
            var targetInstance = instances.Instances.FirstOrDefault(i => i.Model == Model);

            json = await httpClient.GetStringAsync($"https://miot-spec.org/miot-spec-v2/instance?type={targetInstance.Type}");
            Spec = JsonSerializer.Deserialize<Spec>(json);

            return CommunicationResult.Success;
        }

        protected async Task<(CommunicationResult, T?)> SendCommandAsync<T>(ICommand command) where T : BaseResponse
        {
            command.Id = ++DeviceLastCommandId;

            var (result, answerPackage) = await Communicate(new CommandPackage(this, command));

            if (answerPackage != null) {
                var json = await answerPackage.GetPayloadAsync();

                try
                {
                    return (result, JsonSerializer.Deserialize<T>(json));
                }
                catch (Exception ex)
                {
                    //TODO:
                }
            }

            return (result, null);
        }

        private async Task<(CommunicationResult, IPackage?)> Communicate(IPackage package)
        {
            var (result, answerPackage) = await Communication.SendAndReceiveAsync(this, package);

            if (answerPackage != null) {
                DeviceLastCommunicationDateTime = DateTime.UtcNow;
                DeviceLastTimestamp = answerPackage.Timestamp;
            }

            return (result, answerPackage);
        }
    }
}
