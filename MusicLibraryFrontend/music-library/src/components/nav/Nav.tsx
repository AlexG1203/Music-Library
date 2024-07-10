import React from "react";
import { ReactComponent as HomeIcon } from "../../assets/svgs/home.svg";
import { ReactComponent as SearchIcon } from "../../assets/svgs/search.svg";
import { ReactComponent as LogoIcon } from "../../assets/svgs/logo.svg";
import {Link} from "react-router-dom";

import "./navbar.scss"

const Nav: React.FC = () => {
    return (
        <div className="navBar">
            <div className="logo">
                <LogoIcon/>
            </div>
            <ul>
                <Link to="/" style={{ textDecoration: "none" }}>
                    <li>
                        <HomeIcon />
                        Home
                    </li>
                </Link>
                <Link to="/search" style={{ textDecoration: "none" }}>
                    <li>
                        <SearchIcon />
                        Search
                    </li>
                </Link>
            </ul>
            <h1 className="bottomNav">Music Library</h1>
        </div>
    );
};

export default Nav;
