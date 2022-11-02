import { useCallback, useEffect, useRef, useState } from 'react';
import { BlockBlobClient } from '@azure/storage-blob';
import { Button } from 'react-bootstrap';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  const portNumber = 0; /* put your visual studio port number here */;
  const mainUrl = "https://localhost:" + portNumber + "/api/images";

  const [error, setError] = useState();
  const [name, setName] = useState("");
  const [images, setImages] = useState([]);
  const inputFileRef = useRef();

  useEffect(() => {
    if (portNumber <= 0) {
      setError("You need to set your port number in App.js.");
    }
  }, [portNumber]);

  const onClickUpload = useCallback(() => {
    setError("");

    if (!name || name.length < 3) {
        setError("Enter at least three characters for the title.");
        return;
    }

    var file = inputFileRef.current.files[0];
    if (!file) {
        setError("Choose a file.");
        return;
    }

    fetch(mainUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Name: name,
        })
    })
    .then(response => response.json())
    .then(createdImageDetails => {
      const file = inputFileRef.current.files[0];
      const blockBlobClient = new BlockBlobClient(createdImageDetails.uploadUrl);
      return blockBlobClient.uploadData(file);
    }).then(() => {
      // TODO: Send an request to your server that the image upload is complete.
      // Hint: You will need the id of the new image that was created in order to send an upload complete request.
      // That id comes from the POST response that created your new image.
    }).then(uploadCompleteResult => {
      return uploadCompleteResult.json();
    }).then(uploadCompleteJson => {
      // TODO: Use the uploadCompleteJson response to add the new image to the list of images that are displayed on the page.
    });
  }, [mainUrl, name]);

  useEffect(() => {
    if (!mainUrl || portNumber <= 0) {
      return;
    }

    fetch(mainUrl)
      .then(response => {
        if (!response.ok) {
          throw new Error("Not OK status code: " + response.status);
        }
        return response.json()
      })
      .then(responseJson => {
        setImages(responseJson);
      }).catch((error) => {
        setError("Failed to load images on start: " + error);
      });
  }, [mainUrl]);

  const onClickPurge = useCallback(() => {
    fetch(mainUrl, {
      method: "DELETE"
    }).then((result) => {
      if (result.ok) {
        setImages([]);
      }
    });
  }, [mainUrl]);

  return (
    <div className="App">
      <div className="controlsContainer">
          Name: <input value={name} onChange={(e) => setName(e.target.value)} type="text" />

          <input ref={inputFileRef} type="file" accept="image/*" />
      </div>

      <div className="controlsContainer">
        <Button onClick={onClickUpload}>Upload</Button>
        <Button onClick={onClickPurge}>Purge Images</Button>
      </div>

      {error &&
        <div className="error">{error}</div>
      }
      <div className="imagesContainer">
        {/* TODO: use the map function to create one img tag per image in the response from the server.
         * Hint: the "img" element causes the browser to make a GET request to whatever URL is in the src tag.
        */}
      </div>
    </div>
  );
}

export default App;
