Feature: InMemoryParcelTrackersRepository

Allow us to get tracking details for parcels

@tag1
  Scenario: GetTrackingDetails returns the correct tracking details for a parcel
    Given a parcel with ID "LZ712917377US"
    And the parcel has tracking details
    When I call GetTrackingDetails with parcel ID "LZ712917377US"
    Then the result should be a list of tracking details
    And the result should contain the tracking details for the parcel