import React from "react";

import "./main.scss"
import Artists from "../artists/Artists";
import { Routes, Route } from 'react-router-dom';
import Albums from "../albums/Albums";
import Songs from "../songs/Songs";
import Search from "../search/Search";

const Main: React.FC = () => {
    return (
        <div className="main">
            <div className="mainContent">
                <Routes>
                    <Route path="/" element={<Artists/>}/>
                    <Route path="/albums/:artistId" element={<Albums/>}/>
                    <Route path="/songs/:albumId" element={<Songs/>}/>
                    <Route path="/search" element={<Search/>}/>
                </Routes>
            </div>
        </div>
    );
}

export default Main;
