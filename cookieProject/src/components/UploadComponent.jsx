/* eslint-disable no-unused-vars */

import React from "react";
import { useState } from "react";
import "../css/Upload.css";
import * as Yup from "yup";
import { useFormik } from "formik";

const fileCheck = Yup.mixed()
  .test("fileFormat", "Unsupported Format", function (value) {
    if (value) {
      return ["image/jpg", "image/jpeg"].includes(value.type);
    }
    return true;
  })
  .test("fileSize", "File too large", function (value) {
    if (value) {
      return value.size <= 5_000_000;
    }
    return true;
  });

const uploadSchema = Yup.object().shape({
  titel: Yup.string().trim().required().min(3).max(50),
  file: fileCheck,
  file2: fileCheck.nullable(),
  file3: fileCheck.nullable(),
});

function UploadComponent() {
  const [files, setFiles] = useState([]);

  const { values, handleSubmit, handleChange, errors, dirty, isValid } =
    useFormik({
      initialValues: {
        titel: "",
        file: null,
        file2: null,
        file3: null,
      },
      onSubmit: (values) => {
        console.log(values);
        // post request API
        uploadFiles();
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

  const uploadFiles = () => {
    convertAllFiles()
      .then((result) => {
        const convertedFiles = result;
        console.log("convertedFiles:", convertedFiles);
        // postFiles(convertedFiles); // post request naar API
      })
      .catch((error) => {
        console.log(error);
      });
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
          {errors.file ? <p>{errors.file}</p> : null}
        </div>
        <div>
          <input
            className="upload-input"
            type="file"
            name="file2"
            onChange={(event) => handleFileChange(event, 1)}
            style={{ margin: "50px" }}
          />
          {errors.file2 ? <p>{errors.file2}</p> : null}
        </div>
        <div>
          <input
            className="upload-input"
            type="file"
            name="file3"
            onChange={(event) => handleFileChange(event, 2)}
            style={{ margin: "10px" }}
          />
          {errors.file3 ? <p>{errors.file3}</p> : null}
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
