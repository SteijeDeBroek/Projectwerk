/* eslint-disable no-unused-vars */

// exeptie op 0 fotos
import React from "react";
import { useState } from "react";
import "../css/Upload.css";
/*const YourComponent = () => {
  const [buttonText, setButtonText] = useState("Upload");

  const handleClick = () => {
    // Verander de tekst wanneer de knop wordt geklikt
    setButtonText("Geslaagd!");
  };
}; */

function UploadComponent() {
  const [file, setFile] = useState();

  function handleFile(event) {
    setFile(event.target.files[0]);
    console.log(event.target.files[0]);
  }

  function handleUpload() {
    const formData = new FormData();
    formData.append("file", file);
    fetch("url", {
      method: "POST",
      body: formData,
    })
      .then((response) => response.json())
      .then((result) => {
        console.log("succes", result);
      })
      .catch((error) => {
        console.error("Error: ", error);
      });
  }

  return (
    <div className="upload-container">
      <h2 className="text font-serif font-bold text-xl">Upload Files</h2>
      <form onSubmit={handleUpload}>
        <div className="">
          <input
            className="upload-input"
            type="text"
            name="title"
            placeholder="Voeg een titel toe"
            style={{ margin: "50px" }}
          />
        </div>
        <div>
          <input
            className="upload-input"
            type="file"
            name="file"
            onChange={handleFile}
          />
        </div>
        <div>
          {" "}
          <input
            className="upload-input"
            type="file"
            name="file2"
            onChange={handleFile}
            style={{ margin: "50px" }}
          />
        </div>
        <div>
          <input
            className="upload-input"
            type="file"
            name="file3"
            onChange={handleFile}
            style={{ margin: "10px" }}
          />
        </div>

        <button
          style={{ margin: "50px" }}
          className="border border-blue-300 rounded bg-blue-300 p-3  hover:bg-blue-600 focus:bg-blue-600"
        >
          <p className="font font-medium">Upload</p>
        </button>
      </form>
    </div>
  );
}

export default UploadComponent;
