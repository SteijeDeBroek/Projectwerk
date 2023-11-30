import React from "react";
import * as Yup from "yup";
import { useFormik } from "formik";

const loginSchema = Yup.object().shape({
  email1: Yup.string().email().required().trim(),
  password1: Yup.string().required().min(4),
  email2: Yup.string().email().required().trim(),
  password2: Yup.string().required().min(4),
});

const LoginComponent = () => {
  const { values, handleSubmit, handleChange, errors, dirty, isValid } =
    useFormik({
      initialValues: {
        email1: "",
        password1: "",
        email2: "",
        password2: "",
      },
      onSubmit: (values) => {
        console.log(values);
        // post request API
      },
      validationSchema: loginSchema,
    });

  return (
    <>
      <h2 className="flex items-center">Login:</h2>
      <form
        className="flex items-center justify-center p-10 border border-blue-400 rounded  "
        onSubmit={handleSubmit}
        style={{ margin: "50px" }}
      >
        <div>
          <label className="p-5" htmlFor="email1">
            E-mail
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.email1}
            onChange={handleChange}
            type="email"
            placeholder="youremail@gmail.com"
            id="email1"
            name="email1"
          />
          {errors.email1 ? <p>{errors.email1}</p> : null}
        </div>
        <div>
          <label className="p-5" htmlFor="password1">
            Wachtwoord
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.password1}
            onChange={handleChange}
            type="password"
            placeholder="********"
            id="password1"
            name="password1"
          />
          {errors.password1 ? <p>{errors.password1}</p> : null}
        </div>
        <button
          disabled={!isValid || !dirty}
          className="border border-blue-400 rounded  bg-blue-300 hover:bg-blue-600 focus:bg-blue-600 "
          type="submit"
        >
          Aanmelden
        </button>
      </form>
      <h2 className="flex items-center">Registreer:</h2>
      <form
        className="flex items-center justify-center p-10 border border-blue-400 rounded  "
        onSubmit={handleSubmit}
        style={{ margin: "50px" }}
      >
        <div>
          <label className="p-5" htmlFor="email2">
            E-mail
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.email2}
            onChange={handleChange}
            type="email"
            placeholder="youremail@gmail.com"
            id="email2"
            name="email2"
          />
          {errors.email2 ? <p>{errors.email2}</p> : null}
        </div>
        <div>
          <label className="p-5" htmlFor="password2">
            Wachtwoord
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.password2}
            onChange={handleChange}
            type="password"
            placeholder="********"
            id="password2"
            name="password2"
          />
          {errors.password2 ? <p>{errors.password2}</p> : null}
        </div>
        <button
          disabled={!isValid || !dirty}
          className="border border-blue-400 rounded  bg-blue-300 hover:bg-blue-600 focus:bg-blue-600 "
          type="submit"
        >
          Aanmelden
        </button>
      </form>
    </>
  );
};

export default LoginComponent;
