using System;
using System.Collections.Generic;
using System.Windows;

namespace Industrial_SQLite_CRUD_Prototype {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// This controller manages the data flow between the SQLite database and the UI.
  /// </summary>
  public partial class MainWindow : Window {
    // Internal list to hold historical data points
    private List<DeviceDataModel> _historicalData;

    public MainWindow() {
      InitializeComponent();

      try {
        // Step 1: Automatically ensure database and tables exist on startup
        SQLiteDataAccess.InitializeDatabase();

        // Step 2: Perform the initial data load to the UI
        RefreshUI();
      } catch(Exception ex) {
        MessageBox.Show($"Startup Error: {ex.Message}", "System Failure", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    /// <summary>
    /// Fetches the latest logs from the database and updates the ListBox.
    /// </summary>
    private void RefreshUI() {
      try {
        // Load data using our specialized Data Access Layer
        _historicalData = SQLiteDataAccess.LoadData();

        // Bind the data to the UI element
        DataDisplayList.ItemsSource = _historicalData;

        // Force UI refresh to ensure new items are visible
        DataDisplayList.Items.Refresh();
      } catch(Exception ex) {
        MessageBox.Show($"Failed to sync with database: {ex.Message}", "Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    /// <summary>
    /// Simulates a data capture event, typical for an industrial DAQ system.
    /// Triggered by the "Capture New Data" button.
    /// </summary>
    private void CaptureData_Click(object sender, RoutedEventArgs e) {
      try {
        var random = new Random();

        // Create a new data point simulating a sensor reading
        var newData = new DeviceDataModel {
          TagName = "Sensor_A1", // In a real scenario, this would be a Modbus/PLC tag name
          Value = Math.Round(random.NextDouble() * 100, 2),
          Quality = 1, // 1 represents a healthy signal
          Timestamp = DateTime.Now,
          Remark = "Manual trigger from HMI"
        };

        // Save the simulated point to SQLite
        SQLiteDataAccess.SaveData(newData);

        // Refresh the list to show the newly added data
        RefreshUI();
      } catch(Exception ex) {
        MessageBox.Show($"Capture failed: {ex.Message}", "Hardware Simulation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
      }
    }

    /// <summary>
    /// Manual refresh trigger to sync the UI with the latest database state.
    /// </summary>
    private void RefreshUI_Click(object sender, RoutedEventArgs e) {
      RefreshUI();
    }
  }
}