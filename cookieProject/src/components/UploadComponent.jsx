/* eslint-disable no-unused-vars */

// exeptie op 0 fotos
import React from "react";
import { useState } from "react";
import "../css/Upload.css";
import * as Yup from "yup";
import { useFormik } from "formik";

//formik en yup hier alsook toepassen

const uploadSchema = Yup.object().shape({
  titel: Yup.string().trim().required().min(3).max(50),
});

function UploadComponent() {
  const [files, setFiles] = useState([]);

  const { values, handleSubmit, handleChange, errors, dirty, isValid } =
    useFormik({
      initialValues: {
        titel: "",
      },
      onSubmit: (values) => {
        console.log(values);
        // post request API
        handleUpload();
      },
      validationSchema: uploadSchema,
    });

  const handleFileChange = (event, position) => {
    if (event.target.files) {
      console.log("fileObj", event.target.files[0]);
      let tempFIles = [...files];
      tempFIles[position] = event.target.files[0];
      setFiles(tempFIles);
    }
  };

  const convertBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();
      fileReader.readAsDataURL(file);

      fileReader.onload = () => {
        resolve(fileReader.result);
      };

      fileReader.onerror = (error) => {
        reject(error);
      };
    });
  };

  const convertAllFiles = async () => {
    let base64Files = [];
    for (let i = 0; i < files.length; i++) {
      const base64 = await convertBase64(files[i]);
      base64Files.push(base64);
    }
    return base64Files;
  };

  const handleUpload = () => {
    convertAllFiles()
      .then((result) => {
        const convertedFiles = result;
        console.log("convertedFiles:", convertedFiles);
        // postFiles(convertedFiles); // post request naar API
      })
      .catch((error) => {
        console.log(error);
      });

    // const formData = new FormData();
    // formData.append("files", files);
    // fetch("url", {
    //   method: "POST",
    //   body: formData,
    // })
    //   .then((response) => response.json())
    //   .then((result) => {
    //     console.log("succes", result);
    //   })
    //   .catch((error) => {
    //     console.error("Error: ", error);
    //   });
  };

  return (
    <div className="upload-container">
      <h2 className="text font-serif font-bold text-xl">
        Upload jouw gerecht!
      </h2>
      <form
        onSubmit={(event) => {
          event.preventDefault();
          handleSubmit(event);
        }}
      >
        <div className="">
          <input
            className="upload-input"
            type="text"
            name="titel"
            value={values.titel}
            onChange={handleChange}
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
            onChange={(event) => handleFileChange(event, 0)}
          />
        </div>
        <div>
          {" "}
          <input
            className="upload-input"
            type="file"
            name="file2"
            onChange={(event) => handleFileChange(event, 1)}
            style={{ margin: "50px" }}
          />
        </div>
        <div>
          <input
            className="upload-input"
            type="file"
            name="file3"
            onChange={(event) => handleFileChange(event, 2)}
            style={{ margin: "10px" }}
          />
        </div>

        <button
          disabled={!isValid || !dirty}
          style={{ margin: "50px" }}
          className="border border-blue-300 rounded bg-blue-300 p-3  hover:bg-blue-600 focus:bg-blue-600"
          onClick={handleUpload}
        >
          <p className="font font-medium">Upload</p>
        </button>
      </form>
    </div>
  );
}

export default UploadComponent;
