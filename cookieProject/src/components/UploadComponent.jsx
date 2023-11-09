/* eslint-disable no-unused-vars */

// exeptie op 0 fotos
import React from "react";
import { useState } from "react";
import "../css/Upload.css";

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
      <h2>Upload Files</h2>
      <form onSubmit={handleUpload}>
        <input
          className="upload-input"
          type="text"
          name="title"
          placeholder="Voeg een titel toe"
        />
        <input
          className="upload-input"
          type="file"
          name="file"
          onChange={handleFile}
        />
        <input
          className="upload-input"
          type="file"
          name="file2"
          onChange={handleFile}
        />
        <input
          className="upload-input"
          type="file"
          name="file3"
          onChange={handleFile}
        />
        <button className="border border-blue-300 rounded bg-blue-300">
          Upload
        </button>
      </form>
    </div>
  );
}

export default UploadComponent;
