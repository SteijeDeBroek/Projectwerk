import { pages } from "../routes/Pages";
import { Link } from "react-router-dom";

const HeaderComponent = () => {
  const generateLink = (page) => {
    if (page.headerLabel != null) {
      return (
        <li key={page.path}>
          <Link to={page.path}>{page.headerLabel}</Link>
        </li>
      );
    }
    return null;
  };

  return (
    <header className="sticky top-0 z-50">
      <nav aria-label="Global">
        <ul className="border border-blue-400 rounded bg-blue-300 flex flex-1  items-center justify-between p-6 lg:px-8">
          {pages.map((page) => {
            return generateLink(page);
          })}
        </ul>
      </nav>
    </header>
  );
};

export default HeaderComponent;
