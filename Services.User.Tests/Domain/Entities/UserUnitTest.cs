using Services.User.Domain.Events;
using Services.User.Domain.Exceptions;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.Tests.Domain.Entities
{

    public class UserUnitTest
    {
        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Create_User_With_Valid_Parameters()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";

            // Act
            var user = new UserEntity(name, email, password);

            // Assert
            Assert.NotNull(user);
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.True(user.IsActive);
            Assert.Equal(DateTime.Now.Date, user.CreatedAt.Date);
            Assert.Equal(DateTime.Now.Date, user.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Not_Create_User_With_Invalid_Name()
        {
            // Arrange
            var name = "";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";

            // Act
            Action act = () => new UserEntity(name, email, password);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("Name is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Not_Create_User_With_Invalid_Email()
        {
            // Arrange
            var name = "John Doe";
            var email = "";
            var password = "p@ssw0rd";

            // Act
            Action act = () => new UserEntity(name, email, password);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("Email is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Not_Create_User_With_Invalid_Password()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "";

            // Act
            Action act = () => new UserEntity(name, email, password);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("Password is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Add_UserCreated_Event_On_User_Create()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "P@ssw0rd";

            // Act
            var user = new UserEntity(name, email, password);
            var events = user.GetDomainEvents();

            // Assert
            Assert.Single(events);
            var userCreatedEvent = Assert.IsType<UserCreated>(events.First());
            Assert.Equal(user.Id, userCreatedEvent.Id);
            Assert.Equal(name, userCreatedEvent.Name);
            Assert.Equal(email, userCreatedEvent.Email);
        }


        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Update_User_With_Valid_Parameters()
        {
            // Arrange
            var user = new UserEntity("John Doe", "john.doe@example.com", "P@ssw0rd");
            var newName = "John Updated";
            var newPassword = "NewP@ssw0rd";

            // Act
            user.Update(newName, newPassword);

            // Assert
            Assert.Equal(newName, user.Name);
            Assert.Equal(newPassword, user.Password);
            Assert.Equal(DateTime.Now.Date, user.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Not_Update_User_With_Invalid_Name()
        {
            // Arrange
            var user = new UserEntity("John Doe", "john.doe@example.com", "P@ssw0rd");
            var newName = "";
            var newPassword = "NewP@ssw0rd";

            // Act
            Action act = () => user.Update(newName, newPassword);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("Name is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Not_Update_User_With_Invalid_Password()
        {
            // Arrange
            var user = new UserEntity("John Doe", "john.doe@example.com", "P@ssw0rd");
            var newName = "John Updated";
            var newPassword = "";

            // Act
            Action act = () => user.Update(newName, newPassword);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("Password is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Add_UserUpdate_Event_On_User_Update()
        {
            // Arrange
            var user = new UserEntity("John Doe", "john.doe@example.com", "P@ssw0rd");
            var newName = "John Updated";
            var newPassword = "NewP@ssw0rd";

            // Act
            user.Update(newName, newPassword);
            var events = user.GetDomainEvents();

            // Assert
            Assert.Equal(2, events.Count);
            var userUpdatedEvent = Assert.IsType<UserUpdated>(events.Last());
            Assert.Equal(user.Id, userUpdatedEvent.Id);
            Assert.Equal(newName, userUpdatedEvent.Name);
        }

        [Fact]
        [Trait("Domain", "User - Aggregates")]
        public void Should_Mark_User_As_Inactive_On_Delete()
        {
            // Arrange
            var user = new UserEntity("John Doe", "john.doe@example.com", "P@ssw0rd");

            // Act
            user.Delete();

            // Assert
            Assert.False(user.IsActive);
            Assert.Equal(DateTime.Now.Date, user.UpdatedAt.Date);
        }

    }
}
