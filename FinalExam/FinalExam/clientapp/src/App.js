import { useCallback, useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import useLocalStorageState from './useLocalStorageState';

function App() {
  const portNumber = 0; /* put your visual studio port number here; default of the server project is 52693*/;
  const mainUrl = "https://localhost:" + portNumber + "/api/";

  const [error, setError] = useState();
  const [token, setToken] = useLocalStorageState("token");
  const [username, setUsername] = useState("finalexam");
  const [password, setPassword] = useState("12345");
  const [name, setName] = useState("");
  const [genre, setGenre] = useState("");
  const [numberOfPlayers, setNumberOfPlayers] = useState("1");
  const [boardGames, setBoardGames] = useState([]);
  const [apiVersion] = useState("1.0");

  useEffect(() => {
    if (portNumber <= 0) {
      setError("You need to set your port number in App.js.");
    }
  }, [portNumber]);

  const onClickLogin = useCallback(() => {
    fetch(mainUrl + "login?api-version=" + apiVersion, {
      method: "POST",
      headers: {
          "Content-Type": "application/json"
      },
      body: JSON.stringify({
          username: username,
          password: password
      })
    })
    .then(response => response.json())
    .then(loggedInUser => {
      setToken(loggedInUser.token);
      setError("");
    }).catch(() => {
      setError("Failed to login.");
    });
  }, [setToken, mainUrl, apiVersion, password, username]);

  const onClickDelete = useCallback((name, index) => {
    fetch(mainUrl + "boardGames/" + name + "?api-version=" + apiVersion, {
      method: "DELETE",
      headers: {
        Authorization: "Bearer " + token
      }
    })
    .then(response => {
      if (!response.ok) {
        throw Error("response wasn't ok.");
      }

      // response was ok, now update the UI.
      const newBoardGames = [...boardGames];
      newBoardGames.splice(index, 1);
      setBoardGames(newBoardGames)
    }).catch(() => {
      setError("Failed to delete game.");
    });
  }, [mainUrl, apiVersion, token, boardGames]);

  const onClickAdd = useCallback(() => {
    fetch(mainUrl + "boardGames?api-version=" + apiVersion, {
      method: "POST",
      headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token
      },
      body: JSON.stringify({
          name: name,
          genre: genre,
          numberOfPlayers: numberOfPlayers
      })
    })
    .then(response => response.json())
    .then(createdGame => {
      setError("");
      setBoardGames([...boardGames, createdGame]);
    }).catch(() => {
      setError("Failed create game.");
    });
  }, [mainUrl, apiVersion, token, boardGames, name, genre, numberOfPlayers]);

  const onClickLogout = useCallback(() => {
    setToken("");
    setBoardGames([]);
  }, [setToken]);

  useEffect(() => {
    if (!mainUrl || portNumber <= 0 || !token) {
      return;
    }

    fetch(mainUrl + "boardGames?api-version=" + apiVersion, {
      headers: {
        Authorization: "Bearer " + token
      }
    })
      .then(response => {
        if (!response.ok) {
          throw new Error("Not OK status code: " + response.status);
        }
        return response.json()
      })
      .then(responseJson => {
        setBoardGames(responseJson);
      }).catch((error) => {
        setError("Failed to load board games on start: " + error);
        setToken("");
      });
  }, [setToken, mainUrl, apiVersion, token]);

  return (
    <div className="App">
      <div className="loginContainer">
          {!token &&
            <>
              <div>
                (A username/password is filled in here and exists in the database already, so you can login.<br />
                The fact that this password is already filled in is NOT something you should comment on for the code review; it is just here for your convenience.)
              </div>
              <div>
                Username: <input value={username} onChange={(e) => setUsername(e.target.value)} type="text" />
              </div>
              <div>
                Password:  <input value={password} onChange={(e) => setPassword(e.target.value)} type="password" />
              </div>
              <div>
                <Button onClick={onClickLogin}>Login</Button>
              </div>
            </>
          }
          {token && 
            <div>
              <Button onClick={onClickLogout}>Logout</Button>
            </div>
          }
      </div>

      {error &&
        <div className="error">{error}</div>
      }
      <div className="boardGameContainer">
        {boardGames.map((boardGame, index) => 
          <div key={boardGame.name} className="boardGame">
            <div>
              {boardGame.name}
            </div>
            <div>
              {boardGame.genre}
            </div>
            <div>
              {boardGame.numberOfPlayers}
            </div>
            <div>
              <Button variant="danger" onClick={() => onClickDelete(boardGame.name, index)}>Delete</Button>
            </div>
          </div>
        )}
      </div>

      {token &&
        <div className="boardGameAddContainer">
          <div>Add a new game:</div>
          <div>
            Name: <input value={name} onChange={(e) => setName(e.target.value)} type="text" />
          </div>
          <div>
            Genre: <input value={genre} onChange={(e) => setGenre(e.target.value)} type="text" />
          </div>
          <div>
            Number of Players: 
            <select type='text' value={numberOfPlayers} onChange={(e) => setNumberOfPlayers(e.target.value)}>
              <option value="1">1</option>
              <option value="2">2</option>
              <option value="3">3</option>
              <option value="4">4</option>
              <option value="5">5</option>
              <option value="6">6</option>
              <option value="7">7</option>
              <option value="8">8</option>
              <option value="9">9</option>
              <option value="10">10</option>
            </select>
          </div>
          <div>
            <Button variant="success" onClick={onClickAdd}>Add Game</Button>
          </div>
        </div>
      }
    </div>
  );
}

export default App;
