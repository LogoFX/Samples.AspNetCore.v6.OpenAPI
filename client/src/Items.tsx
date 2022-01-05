import { WeatherForecast } from "./generated";
import Item from "./Item";

interface ItemsProps {
    items: WeatherForecast[]
}
const Items: React.FC<ItemsProps> = ({ items }) => {
    return (
        <table style={{columnGap: 120, width: "100%", maxWidth: 480}}>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>TemperatureC</th>
                    <th>TemperatureF</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {items.map((item, index) => (
                    <Item key={`item_${index}`} item={item} />
                ))}
            </tbody>
        </table>
    );
}

export default Items;