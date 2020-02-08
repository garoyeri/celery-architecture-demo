import React, { useState, useEffect } from "react";

export function Weather() {
  const [forecasts, setForecasts] = useState([]);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    (async () => {
      const response = await fetch("api/Weather/Forecasts");
      const data = await response.json();
      setForecasts(data);
      setLoading(false);
    })();
  }, []);
  function renderForecastsTable(forecasts) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast => (
            <tr key={forecast.date}>
              <td>{forecast.dateFormatted}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }
  function renderLoadingMessage() {
    return (
      <p>
        <em>Loading...</em>
      </p>
    );
  }
  return (
    <div>
      <h1 id="tableLabel">Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {loading ? renderLoadingMessage() : renderForecastsTable(forecasts)}
    </div>
  );
}
