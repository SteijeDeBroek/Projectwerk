import React, { useState } from "react";

const LoginComponent = () => {
  const [email, setEmail] = useState("");
  const [pass, setPass] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(email); // TODO: API aanspreken
  };

  return (
    <form onSubmit={handleSubmit}>
      <label htmlFor="e-mail">E-mail</label>
      <input
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        type="email"
        placeholder="youremail@gmail.com"
        id="e-mail"
        name="e-mail"
      />
      <label htmlFor="wachtwoord">Wachtwoord</label>
      <input
        value={pass}
        onChange={(e) => setPass(e.target.value)}
        type="password"
        placeholder="********"
        id="wachtwoord"
        name="wachtwoord"
      />
      <button type="submit">Aanmelden</button>
    </form>
  );
};

export default LoginComponent;
