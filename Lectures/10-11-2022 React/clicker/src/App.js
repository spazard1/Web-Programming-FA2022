import './App.css';
import ClickCounter from './components/ClickCounter';
import NumberList from './components/NumberList';


const App = () => {
  return (
    <div className="App">
      Hello, World!
      <ClickCounter />
      <ClickCounter />
      
      <NumberList />
    </div>
  );
}

export default App;
