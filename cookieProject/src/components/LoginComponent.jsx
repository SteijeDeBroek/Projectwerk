import React, { useState } from "react";
import "../css/Login.css";

const LoginComponent = () => {
  const [email, setEmail] = useState("");
  const [pass, setPass] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(email); // TODO: API aanspreken
  };

  return (
    <form
      className="flex items-center justify-center p-10 "
      onSubmit={handleSubmit}
    >
      <label className="p-5" htmlFor="e-mail">
        E-mail
      </label>
      <input
        className="border border-gray-400 rounded"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        type="email"
        placeholder="youremail@gmail.com"
        id="e-mail"
        name="e-mail"
      />
      <label className="p-5" htmlFor="wachtwoord">
        Wachtwoord
      </label>
      <input
        className="border border-gray-400 rounded"
        value={pass}
        onChange={(e) => setPass(e.target.value)}
        type="password"
        placeholder="********"
        id="wachtwoord"
        name="wachtwoord"
      />
      <button
        className="border border-blue-400 rounded  bg-blue-300 "
        type="submit"
      >
        Aanmelden
      </button>
    </form>
  );
};

export default LoginComponent;
