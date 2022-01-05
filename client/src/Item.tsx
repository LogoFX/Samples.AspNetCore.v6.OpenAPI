import { WeatherForecast } from "./generated";

interface ItemsProps {
    item: WeatherForecast;
}
const Item: React.FC<ItemsProps> = ({ item }) => {
    return (
        <tr>
            <td>{new Date(item.date).toLocaleDateString()}</td>
            <td>{item.temperatureC}</td>
            <td>{item.temperatureF}</td>
            <td>{item.summary}</td>
        </tr>
    )
}

export default Item;