import React from 'react';
import logo from './logo.svg';
import './App.css';
import { WeatherForecast, WeatherForecastClient } from "./generated";
import { useEffect, useState } from 'react';
import Items from "./Items";

const client = new WeatherForecastClient("https://localhost:7090/api/v1");

function App() {
  const [isBusy, setIsBusy] = useState(false);
  const [items, setItems] = useState<WeatherForecast[]>([]);


  const getWeatherForecast = async () => {
    setIsBusy(true);
    try {
      const items = await client.getWeatherForecast();
      setItems(items);
    } catch (err) {
      console.log(err);
      alert("Cannot fetch.");
    } finally {
      setIsBusy(false);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
      </header>
      <main>
        {isBusy ? (
          <img src={logo} className="App-logo" alt="logo" />
        ) : (
          <div>
            <button onClick={getWeatherForecast}>Get Weather Forecast</button>
            <Items items={items} />
          </div>          
        )}        
      </main>
    </div>
  );
}

export default App;
