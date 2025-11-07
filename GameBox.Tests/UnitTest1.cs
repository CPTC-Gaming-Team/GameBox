using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameBox.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace GameBox.Tests
{
    [TestClass]
    public class GameModelTests
    {
        [TestMethod]
        public void GameModel_Properties_ShouldBeSettable()
        {
            // Arrange & Act
            var game = new GameModel
            {
                Name = "Test Game",
                Description = "Test Description",
                Rating = 5
            };

            // Assert
            Assert.AreEqual("Test Game", game.Name);
            Assert.AreEqual("Test Description", game.Description);
            Assert.AreEqual(5, game.Rating);
        }

        [TestMethod]
        public void GameModel_Id_ShouldHaveDefaultValue()
        {
            // Arrange & Act
            var game = new GameModel
            {
                Name = "Test Game",
                Description = "Test Description",
                Rating = 5
            };

            // Assert
            Assert.AreEqual(0, game.Id);
        }

        [TestMethod]
        public void GameModel_ValidName_ShouldPassValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Game Name",
                Description = "Valid Description",
                Rating = 5
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void GameModel_NameExceeding100Characters_ShouldFailValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = new string('A', 101),
                Description = "Valid Description",
                Rating = 5
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.MemberNames.Contains("Name")));
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Game name cannot exceed 100 characters")));
        }

        [TestMethod]
        public void GameModel_NameAt100Characters_ShouldPassValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = new string('A', 100),
                Description = "Valid Description",
                Rating = 5
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void GameModel_DescriptionExceeding1000Characters_ShouldFailValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Name",
                Description = new string('B', 1001),
                Rating = 5
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.MemberNames.Contains("Description")));
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Description cannot exceed 1000 characters")));
        }

        [TestMethod]
        public void GameModel_DescriptionAt1000Characters_ShouldPassValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Name",
                Description = new string('B', 1000),
                Rating = 5
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void GameModel_EmptyDescription_ShouldPassValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Name",
                Description = string.Empty,
                Rating = 5
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void GameModel_RatingBelowMinimum_ShouldFailValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Rating = 0
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.MemberNames.Contains("Rating")));
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Ratings must be between 1 and 10")));
        }

        [TestMethod]
        public void GameModel_RatingAboveMaximum_ShouldFailValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Rating = 11
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.MemberNames.Contains("Rating")));
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Ratings must be between 1 and 10")));
        }

        [TestMethod]
        public void GameModel_RatingAtMinimum_ShouldPassValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Rating = 1
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void GameModel_RatingAtMaximum_ShouldPassValidation()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Rating = 10
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void GameModel_ValidModel_ShouldPassAllValidations()
        {
            // Arrange
            var game = new GameModel
            {
                Name = "Great Game",
                Description = "This is a great game with an awesome description.",
                Rating = 8
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Helper method to validate a model using DataAnnotations
        /// </summary>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
