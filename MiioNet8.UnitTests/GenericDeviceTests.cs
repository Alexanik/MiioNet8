using MiioNet8.Devices;
using MiioNet8.Interfaces;
using MiioNet8.Protocol;
using Moq;

namespace MiioNet8.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GenericDeviceConnectionTestAsync()
        {
            var communication = new Mock<ICommunication>();
            communication.Setup(c => c.SendAndReceiveAsync(It.IsAny<IDevice>(), It.IsAny<IPackage>()))
                .ReturnsAsync((Communication.CommunicationResult.Error, null));
            communication.Setup(c => c.SendAndReceiveAsync(It.IsAny<IDevice>(), It.Is<IPackage>(p => p is HelloPackage)))
                .ReturnsAsync((IDevice device, IPackage package) => 
                {
                    var answer = new AnswerPackage(device, [0x21, 0x31, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF]);

                    return (Communication.CommunicationResult.Success, answer);
                });
            communication.Setup(c => c.SendAndReceiveAsync(It.IsAny<IDevice>(), It.Is<IPackage>(p => p is CommandPackage)))
                .ReturnsAsync((IDevice device, IPackage package) =>
                {
                    return (Communication.CommunicationResult.Success, null);
                });

            var token = new Mock<IToken>();
            token.Setup(t => t.ToString()).Returns("000102030405060708090A0B0C0D0E0F");
            token.Setup(t => t.ToByteArray()).Returns([0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F]);

            var testDevice = new GenericDevice(communication.Object, token.Object);
          
            await testDevice.ConnectAsync();

            Assert.Pass();
        }
    }
}