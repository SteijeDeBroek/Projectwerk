/* eslint-disable no-unused-vars */

// exeptie op 0 fotos
import React from "react";
import { useState } from "react";
import "../css/Upload.css";
import * as Yup from "yup";
import { useFormik } from "formik";
/*const YourComponent = () => {
  const [buttonText, setButtonText] = useState("Upload");

  const handleClick = () => {
    // Verander de tekst wanneer de knop wordt geklikt
    setButtonText("Geslaagd!");
  };
}; */

//formik en yup hier alsook toepassen

const uploadSchema = Yup.object().shape({
  titel: Yup.string().trim().required().min(3).max(50),
});

function UploadComponent() {
  const [file, setFile] = useState();
  const { values, handleSubmit, handleChange, errors, dirty, isValid } =
    useFormik({
      initialValues: {
        titel: "",
      },
      onSubmit: (values) => {
        console.log(values);
        // post request API
      },
      validationSchema: uploadSchema,
    });
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
      <h2 className="text font-serif font-bold text-xl">
        Upload jouw gerecht!
      </h2>
      <form>
        <div className="">
          <input
            className="upload-input"
            type="text"
            name="titel"
            value={values.titel}
            onChange={handleChange}
            onSubmit={handleSubmit}
            placeholder="Voeg een titel toe"
            style={{ margin: "50px" }}
          />
          {errors.titel ? <p>{errors.titel}</p> : null}
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
          disabled={!isValid || !dirty}
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
