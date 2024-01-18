import { pages } from "../routes/Pages";
import { Link } from "react-router-dom";
import logo from "../assets/logo.png";

const HeaderComponent = () => {
  const linkerkant = ["Home", "Upload", "Vote"]; // Links voor de linkerkant
  const rechterkant = ["Login"]; // Link voor de rechterkant

  const generateLink = (page) => {
    if (page.headerLabel != null) {
      return (
        <li key={page.path} className="mx-3">
          <Link
            to={page.path}
            className="text-white transition duration-300 ease-in-out hover:underline"
          >
            {page.headerLabel}
          </Link>
        </li>
      );
    }
    return null;
  };

  return (
    <header className="sticky top-0 z-10 bg-blue-800 shadow-lg text-white">
      <nav
        aria-label="Global"
        className="flex justify-between items-center p-4 lg:px-8"
      >
        <div className="flex items-center">
          <Link to="/" className="flex items-center">
            <img
              src={logo}
              alt="Culinary Clash Logo"
              className="h-16 lg:h-24 mr-4"
            />
          </Link>
          <ul className="flex">
            {pages
              .filter((page) => linkerkant.includes(page.headerLabel))
              .map(generateLink)}
          </ul>
        </div>
        <ul className="flex">
          {pages
            .filter((page) => rechterkant.includes(page.headerLabel))
            .map(generateLink)}
        </ul>
      </nav>
    </header>
  );
};

export default HeaderComponent;
