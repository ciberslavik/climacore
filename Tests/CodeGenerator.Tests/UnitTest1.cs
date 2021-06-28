using System;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services.Communication;
using NUnit.Framework;
using ProtocolGenerator.DataModel;

namespace CodeGenerator.Tests
{
    public class Tests
    {
        private ICommunicationSerializer _serializer;
        
        [SetUp]
        public void Setup()
        {
            _serializer = new NewtonsoftCommunicationSerializer();
        }

        [Test]
        public void Test1()
        {
            Service authService = new Service();
            authService.ServiceName = "AuthorizationService";
            
            Property usersProp = new Property();
            usersProp.PropertyName = "Users";
            usersProp.PropertyType = "List<User>";
            
            authService.Properties.Add(usersProp);
            
            MethodParameter loginParameter = new MethodParameter();
            loginParameter.ParameterName = "login";
            loginParameter.ParameterType = "string";
            
            Method getUserByName = new Method();
            getUserByName.MethodName = "GetUserByName";
            getUserByName.Parameters.Add(loginParameter);
            getUserByName.ReturnType = "User";
            authService.Methods.Add(getUserByName);

            string data = _serializer.Serialize(authService);
            
            Console.WriteLine(data);
            Assert.Pass();
        }
    }
}