﻿using System.Diagnostics.CodeAnalysis;

namespace parcelfy.Core.DTOs;

[ExcludeFromCodeCoverage]
public class ParcelTrackerDTO
{
	public int ReturnCode { get; set; }
	public string ReturnMessage { get; set; }
	public string IdShip { get; set; }
	public ShipmentDomain Shipment { get; set; }

	public class ShipmentDomain
	{
		public string IdShip { get; set; }
		public string Product { get; set; }
		public bool IsFinal { get; set; }
		public List<Event> Event { get; set; } = [];
		public string Url { get; set; }
	}

	public class Event
	{
		public string Code { get; set; }
		public string Label { get; set; }
		public DateTime Date { get; set; }
	}
}
