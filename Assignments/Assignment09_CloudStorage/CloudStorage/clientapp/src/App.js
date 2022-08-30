import { useCallback, useEffect, useRef, useState } from 'react';
import { BlockBlobClient } from '@azure/storage-blob';
import { Button, ProgressBar } from 'react-bootstrap';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  const mainUrl = "https://localhost:53037/api/images";

  const [apiVersion] = useState("?api-version=1.0");
  const [error, setError] = useState();
  const [name, setName] = useState("");
  const inputFileRef = useRef();
  const [images, setImages] = useState([]);
  const [imageSize, setImageSize] = useState(0);
  const [progress, setProgress] = useState(0);

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

    fetch(mainUrl + apiVersion, {
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
      return blockBlobClient.uploadData(file, {
        onProgress: (ev) => {
          setProgress(Math.round(ev.loadedBytes / file.size * 100));
        }
      });
    }).then(() => {
      return fetch(mainUrl + "/" + createdImageId + "/uploadComplete" + apiVersion, {
        method: "PUT"
      })
    }).then(uploadCompleteResult => {
      return uploadCompleteResult.json();
    }).then(uploadCompleteJson => {
      setProgress(0);
      setImages(ims => [...ims, uploadCompleteJson]);
    });
  }, [name, apiVersion]);

  useEffect(() => {
    if (!apiVersion || !mainUrl) {
      return;
    }

    fetch(mainUrl + apiVersion)
      .then(response => response.json())
      .then(responseJson => {
        setImages(responseJson);
      });
  }, [apiVersion]);

  const onClickPurge = useCallback(() => {
    fetch(mainUrl + apiVersion, {
      method: "DELETE"
    }).then((result) => {
      if (result.ok) {
        setImages([]);
      }
    });
  }, [apiVersion]);


  return (
    <div className="App">
      <div className="controlsContainer">
          Name: <input value={name} onChange={(e) => setName(e.target.value)} type="text" />

          <input ref={inputFileRef} type="file" accept="image/*" />
      </div>

      <div className="controlsContainer">
        <Button onClick={onClickUpload}>Upload</Button>
        <Button onClick={onClickPurge}>Purge Images</Button>
        <input type="range" value={imageSize} onChange={(e) => setImageSize(e.target.value)} min="0" max="5" />
      </div>

      {progress > 0 &&
        <div className="progressContainer">
          <ProgressBar now={progress} />
        </div>
      }

      {error &&
        <div className="error">{error}</div>
      }
      <div className="imagesContainer">
        {images.map(image => 
          <div key={image.id}>
            <img src={mainUrl + "/" + image.id} className={"size" + imageSize} alt={image.name} />
          </div>
        )}
      </div>
    </div>
  );
}

export default App;
