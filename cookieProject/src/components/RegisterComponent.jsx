import * as Yup from "yup";
import { useFormik } from "formik";
import bcrypt from "bcryptjs";
import { useNavigate } from "react-router-dom";

const loginSchema = Yup.object().shape({
  username: Yup.string().required().trim(),
  email: Yup.string().email().required().trim(),
  password: Yup.string().required().min(8),
  confirmPassword: Yup.string()
    .required()
    .oneOf([Yup.ref("password"), null], "Passwords must match"),
});

const RegisterComponent = () => {
  const navigate = useNavigate();

  const { values, handleSubmit, handleChange, errors, dirty, isValid } =
    useFormik({
      initialValues: {
        username: "",
        email: "",
        password: "",
        confirmPassword: "",
      },
      onSubmit: async (values) => {
        try {
          // Hash the password
          const hashedPassword = bcrypt.hashSync(values.password, 15);
          console.log(hashedPassword);
          // hashed gegevens doorsturen
          // const response = await fetch("(invoegen API-Call)", {
          //   // samen kijken welke API call hiervoor zorgt dat gegevens juist worden ingevoegd
          //   method: "POST",
          //   body: JSON.stringify({
          //     email: values.email,
          //     password: hashedPassword,
          //   }),
          // });

          // const result = await response.json();

          // if (result.success) {
          //   console.log("User registered successfully");
          // } else {
          //   console.error("Registration failed:", result.error);
          // }
        } catch (error) {
          console.error("Error:", error);
        }
      },
      validationSchema: loginSchema,
    });

  return (
    <div>
      <h2 className="flex items-center">Registreer</h2>
      <form
        className="flex items-center justify-center p-10 border border-blue-400 rounded  "
        onSubmit={handleSubmit}
        style={{ margin: "50px" }}
      >
        <div>
          <label className="p-5" htmlFor="username">
            Gebruikersnaam
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.username}
            onChange={handleChange}
            type="text"
            placeholder="JohnDoe"
            id="username"
            name="username"
          />
          {errors.username ? <p>{errors.username}</p> : null}
        </div>
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
        <div>
          <label className="p-5" htmlFor="password">
            Bevestig wachtwoord
          </label>
          <input
            className="border border-gray-400 rounded"
            value={values.confirmPassword}
            onChange={handleChange}
            type="password"
            placeholder="********"
            id="confirmPassword"
            name="confirmPassword"
          />
          {errors.confirmPassword ? <p>{errors.confirmPassword}</p> : null}
        </div>
        <button
          disabled={!isValid || !dirty}
          className="border border-blue-400 rounded  bg-blue-300 hover:bg-blue-600 focus:bg-blue-600 "
          type="submit"
        >
          Registreer
        </button>

        <a onClick={() => navigate("/login")} className="p-5">
          Heb je al een login? Meld je aan!
        </a>
      </form>
    </div>
  );
};

export default RegisterComponent;
