using System;
using parcelfy.Core.DTOs;
using parcelfy.Core.Validators;
using Xunit;

namespace parcelfy.tests.Core.Validator;

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
		var parcelTrackerHistory = new ParcelTrackerHistoryDTO
		{
			parcelid = "LU6580211095FR",
			eventcode = "DR1",
			eventmessage = "La Poste est prête à prendre en charge votre envoi.Dès qu’il nous sera confié,",
			eventdate = DateTime.Now,
			product = "Courrier international",
			isfinal = true,
			url = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR"
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
		var parcelTrackerHistory = new ParcelTrackerHistoryDTO
		{
			parcelid = "",
			eventcode = "",
			eventmessage = null,
			eventdate = DateTime.Now,
			product = null,
			isfinal = false,
			url = null
		};

		// Act
		var result = _parcelTrackerHistoryValidatorMock.Validate(parcelTrackerHistory);

		// Assert
		Assert.False(result.IsValid);
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'Parcel Id' ne doit pas être vide.");
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'Parcel Id' doit contenir entre 13 et 16 caractères. 0 caractères ont été saisis.");
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'Event Code' ne doit pas être vide.");
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'Event Code' doit être d’une longueur de 3 caractères. 0 caractères ont été saisis.");
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'Event Message' ne doit pas être vide.");
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'Product' ne doit pas être vide.");
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'Is Final' ne doit pas être vide.");
		//Assert.Contains(result.Errors, x => x.ErrorMessage == "'URL' ne doit pas être vide.");
	}
}

