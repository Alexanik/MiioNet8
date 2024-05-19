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
    public class GenericDevice : BaseDevice
    {
        public GenericDevice(IPAddress iPAddress, int port, Token token) : base(iPAddress, port, token)
        {
        }

        protected async Task<(CommunicationResult, List<Property>?)> GetPropertiesAsync(List<ISpecServiceProperty> properties)
        {
            var (result, response) = await SendCommandAsync<GetPropertiesResponse>(
                new GetPropertiesCommand(properties)
            );

            if (result != CommunicationResult.Success)
                return (result, null);

            return (result, response?.Result);
        }

        protected async Task<T> GetPropertyAsync<T>(ISpecServiceProperty property) where T : struct
        {
            var (communicationResult, result) = await GetPropertiesAsync([property]);

            if (communicationResult != CommunicationResult.Success || result?.Count != 1)
                throw new Exception("");

            if (result[0].Value is JsonElement jsonElement)
            {
                var value = jsonElement.Deserialize<T>();

                return value;
            }

            throw new Exception();
        }

        protected async Task<T> GetPropertyAsync<T>(string serviceName, string propertyName) where T : struct
        {
            if (!GetSpecServiceProperty(serviceName, propertyName, out var property))
                throw new Exception("");

            return await GetPropertyAsync<T>(property!);
        }

        protected async Task SetPropertiesAsync(List<(ISpecServiceProperty, object)> propertiesWithValue)
        {
            var (result, response) = await SendCommandAsync<BaseResponse>(
                new SetPropertiesCommand(propertiesWithValue)
            );
        }

        protected async Task SetPropertyAsync(ISpecServiceProperty property, object value)
        {
            await SetPropertiesAsync([(property, value)]);
        }

        protected async Task SetPropertyAsync(string serviceName, string propertyName, object value)
        {
            if (!GetSpecServiceProperty(serviceName, propertyName, out var property))
                throw new Exception("");

            await SetPropertyAsync(property!, value);
        }

        protected bool GetSpecServiceProperty(string serviceName, string propertyName, out ISpecServiceProperty? property)
        {
            property = Spec.Properties.FirstOrDefault(p => p.Service.Description.ToLower() == serviceName.ToLower() && p.Description.ToLower() == propertyName.ToLower());

            return property != null;
        }
    }
}
