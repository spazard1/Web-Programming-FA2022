import { useCallback, useEffect, useRef, useState } from 'react';
import { BlockBlobClient } from '@azure/storage-blob';
import { Button } from 'react-bootstrap';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  const portNumber = 44303; /* put your visual studio port number here */;
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
    let createdImageId;
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
      createdImageId = createdImageDetails.id;
      const file = inputFileRef.current.files[0];
      const blockBlobClient = new BlockBlobClient(createdImageDetails.uploadUrl);
      return blockBlobClient.uploadData(file);
    }).then(() => {
      return fetch(mainUrl + "/" + createdImageId + "/uploadComplete", {
        method: "PUT"
      })
    }).then(uploadCompleteResult => {
      return uploadCompleteResult.json();
    }).then(uploadCompleteJson => {
      setImages(ims => [...ims, uploadCompleteJson]);
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
        {images.map(image => 
          <div key={image.id}>
            <img src={mainUrl + "/" + image.id} alt={image.name} />
          </div>
        )}
      </div>
    </div>
  );
}

export default App;
