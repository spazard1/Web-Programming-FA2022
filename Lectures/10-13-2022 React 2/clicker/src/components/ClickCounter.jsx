import React, { useState } from "react";
import { useEffect } from "react";
import { Button } from "react-bootstrap";

import "./ClickCounter.css";

const ClickCounter = ({onScoreChange, teamName}) => {
  const [counterValue, setCounterValue] = useState(0);

  const increment = () => {
    setCounterValue(counterValue + 1);
  }

  const decrement = () => {
    setCounterValue(counterValue - 1);
  }

  useEffect(() => {
    onScoreChange(teamName, counterValue);
  }, [onScoreChange, teamName, counterValue]);

  return (
    <div className="clickCounterContainer">
      <div>{teamName}: {counterValue}</div>
      <Button onClick={increment}>++</Button>
      <Button disabled={counterValue <= 0} onClick={decrement}>--</Button>
    </div>
  );
}

export default ClickCounter;
