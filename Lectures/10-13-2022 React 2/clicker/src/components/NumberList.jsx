import React, { useState } from "react";
import { Button } from "react-bootstrap";

import "./NumberList.css";

const NumberList = () => {
  const [numbers, setNumbers] = useState([]);

  const addNumber = () => {
    setNumbers([...numbers, Math.random()])
  }

  const deleteNumber = (index) => {
    const newNumbers = [...numbers];
    newNumbers.splice(index, 1);
    setNumbers(newNumbers);
  }

  return (
    <>
      <Button onClick={addNumber}>Add Number</Button>
      <div className="numberListContainer">
        {numbers.map((element, index) => 
          <div key={element} onClick={() => deleteNumber(index)}>{element}</div>
        )}
      </div>
    </>
  );
}

export default NumberList;