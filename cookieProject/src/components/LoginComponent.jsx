import * as Yup from "yup";
import { useFormik } from "formik";
import { useNavigate } from "react-router-dom";
import bcrypt from "bcryptjs";

const loginSchema = Yup.object().shape({
  email: Yup.string().email().required().trim(),
  password: Yup.string().required(),
});

const LoginComponent = () => {
  const navigate = useNavigate();

  const { values, handleSubmit, handleChange, errors, dirty, isValid } =
    useFormik({
      initialValues: {
        email: "",
        password: "",
        email2: "",
        password2: "",
      },
      onSubmit: async (values) => {
        try {
          // FIXME: API call to check login credentials
          const hashedPassword = bcrypt.hashSync("Test123", 15);

          if (bcrypt.compareSync(values.password, hashedPassword)) {
            console.log("Login successful");
          } else {
            console.error("Login failed");
          }
        } catch (error) {
          console.error("Error:", error);
        }
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
          <label className="p-5" htmlFor="email">
            E-mail
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.email}
            onChange={handleChange}
            type="email"
            placeholder="youremail@gmail.com"
            id="email"
            name="email"
          />
          {errors.email ? <p>{errors.email}</p> : null}
        </div>
        <div>
          <label className="p-5" htmlFor="password">
            Wachtwoord
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.password}
            onChange={handleChange}
            type="password"
            placeholder="********"
            id="password"
            name="password"
          />
          {errors.password ? <p>{errors.password}</p> : null}
        </div>
        <button
          disabled={!isValid || !dirty}
          className="border border-blue-400 rounded  bg-blue-300 hover:bg-blue-600 focus:bg-blue-600 "
          type="submit"
        >
          Aanmelden
        </button>
        <a onClick={() => navigate("/register")} className="p-5">
          Nog geen login? Registreer nu!
        </a>
      </form>
    </>
  );
};

export default LoginComponent;
