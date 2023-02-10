using System;
using System.Linq;
using parcelfy.Application.ParcelTrackers.Models;
using Xunit;

namespace Application.ParcelTrackers.Models;

public class ParcelTrackerHistoryValidatorTests
{
	private readonly ParcelTrackerHistoryValidator _parcelTrackerHistoryValidatorMock;

	public ParcelTrackerHistoryValidatorTests()
	{
		_parcelTrackerHistoryValidatorMock = new ParcelTrackerHistoryValidator();
	}

	[Fact]
	public void ParcelTrackerHistoryValidator_Validate_ValidParcelTrackerHistory_ShouldHaveNoValidationError()
	{
		// Arrange
		var parcelTrackerHistory = new ParcelTrackerHistory
		{
			ParcelId = "LU6580211095FR",
			EventCode = "DR1",
			EventMessage = "La Poste est prête à prendre en charge votre envoi.Dès qu’il nous sera confié,",
			EventDate = DateTime.Now,
			Product = "Courrier international",
			IsFinal = true,
			URL = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR"
		};

		// Act
		var result = _parcelTrackerHistoryValidatorMock.Validate(parcelTrackerHistory);

		// Assert
		Assert.True(result.IsValid);
	}

	[Fact]
	public void ParcelTrackerHistoryValidator_Validate_InvalidParam_ShouldHaveValidationErrors()
	{
		// Arrange
		var parcelTrackerHistory = new ParcelTrackerHistory
		{
			ParcelId = "",
			EventCode = "",
			EventMessage = null,
			EventDate = DateTime.Now,
			Product = null,
			IsFinal = false,
			URL = null
		};

		// Act
		var result = _parcelTrackerHistoryValidatorMock.Validate(parcelTrackerHistory);

		// Assert
		Assert.False(result.IsValid);
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'Parcel Id' must not be empty.");
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'Parcel Id' must be between 14 and 16 characters. You entered 0 characters.");
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'Event Code' must not be empty.");
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'Event Code' must be 3 characters in length. You entered 0 characters.");
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'Event Message' must not be empty.");
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'Product' must not be empty.");
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'Is Final' must not be empty.");
		Assert.Contains(result.Errors, x => x.ErrorMessage == "'URL' must not be empty.");
	}
}

