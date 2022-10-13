import React, { useCallback, useEffect, useState } from "react";
import './App.css';
import ClickCounter from './components/ClickCounter';
import NumberList from './components/NumberList';

const App = () => {
  const [teamScores, setTeamScores] = useState({});

  const onScoreChange = useCallback((teamName, score) => { 
    setTeamScores((currentTeamScores) => {
      const newTeamScores = {...currentTeamScores};
      newTeamScores[teamName] = score;
      return currentTeamScores;
    });
  }, []);

  useEffect(() => {
    const titles = [];

    for (const [teamName, score] of Object.entries(teamScores)) {
      titles.push(teamName + ":" + score);
    }

    document.title = titles.join(" ");
  }, [teamScores]);

  return (
    <div className="App">
      Hello, World!
      <ClickCounter onScoreChange={onScoreChange} teamName="Great Team" />
      <ClickCounter onScoreChange={onScoreChange} teamName="Better Team" />
      
      <NumberList />
    </div>
  );
}

export default App;
