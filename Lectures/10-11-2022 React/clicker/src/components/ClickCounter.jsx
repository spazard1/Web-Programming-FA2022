import React, { useState } from "react";
import { Button } from "react-bootstrap";

import "./ClickCounter.css";

const ClickCounter = () => {
  const [counterValue, setCounterValue] = useState(0);

  const increment = () => {
    setCounterValue(counterValue + 1);
  }

  const decrement = () => {
    setCounterValue(counterValue - 1);
  }

  return (
    <div className="clickCounterContainer">
      <div>Counter Value: {counterValue}</div>
      <Button onClick={increment}>++</Button>
      <Button disabled={counterValue <= 0} onClick={decrement}>--</Button>
    </div>
  );
}

export default ClickCounter;
