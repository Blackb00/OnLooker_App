using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnLooker.Core;
using OnLooker.DataBaseAccess;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests
{
    [TestClass]
    public class CRUDUserInfoLoginTests
    {
        private CUserInfoGateway _userInfoGateway;
        private CUserLoginGateway _usrLoginGateway;
        private UserInfo _user;
        private UserAuthInfo _login;

       [TestInitialize]
        public void Initialize()
        {
            CDbDeploy.ConnectDataBase();
            _userInfoGateway = new CUserInfoGateway();
            _usrLoginGateway = new CUserLoginGateway();
            _user = new UserInfo()
            {
                Email = "testEmail@mail.ru",
                Name = "TestName"
            };
            _login = new UserAuthInfo()
            {
                Login = "testLogin",
                Password = "password"
            };
        }
        [TestMethod]
        public void ShouldCreateAndReadUser()
        {
            //Act
            _user.ID = _userInfoGateway.Create(_user, _login);
            var user = _userInfoGateway.Get(_user.ID);

            //Assert
            IsTrue(String.Equals(_user.Email,user.Email, StringComparison.CurrentCulture) && String.Equals(_user.Name, user.Name, StringComparison.CurrentCulture));

        }

        [TestCleanup]
        public void DeleteTestData()
        {
           _userInfoGateway.Delete(_user.ID);
           _usrLoginGateway.Delete(_login.ID);
        }

        [TestMethod]
        public void ShouldUpdateUser()
        {
            //Arrange
            _user.ID = _userInfoGateway.Create(_user, _login);
            var _userChanged = new UserInfo()
            {
                Email = "testChangedEmail@mail.ru",
                Name = "TestName",
                ID = _user.ID
            };
            //Act 
            _userInfoGateway.Update(_userChanged);
            var user = _userInfoGateway.Get(_user.ID);
            //Assert
            IsTrue(String.Equals(_user.Name, user.Name, StringComparison.CurrentCulture));
            IsFalse(String.Equals(_user.Email, user.Email, StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ShouldDeleteUserLogin()
        {
            //Arrange
            _user.ID = _userInfoGateway.Create(_user, _login);
            _login.ID = _usrLoginGateway.GetByUserId(_user.ID);
            //Act
            _usrLoginGateway.Delete(_login.ID);
            var user = _userInfoGateway.Get(_user.ID);
            //Assert
            IsTrue(user==null);
        }
    }

}
