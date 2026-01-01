using System;

namespace Industrial_SQLite_CRUD_Prototype {
  /// <summary>
  /// Represents a single historical data point captured from industrial sensors/devices.
  /// </summary>
  public class DeviceDataModel {
    // Primary identifier for the database record
    public int Id {
      get; set;
    }

    // The identifier of the sensor or signal (e.g., "Pressure_Sensor_01")
    public string TagName {
      get; set;
    }

    // The numerical value captured from the device
    public double Value {
      get; set;
    }

    // Data quality indicator: 1 = Valid, 0 = Invalid/Error
    public int Quality {
      get; set;
    }

    // Precise time when the data was recorded
    public DateTime Timestamp { get; set; } = DateTime.Now;

    // Optional notes regarding the specific data point
    public string Remark {
      get; set;
    }

    // Formatted string for UI display purposes
    public string DisplayInfo => $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {TagName}: {Value} (Quality: {Quality})";

    public override string ToString() => DisplayInfo;
  }
}