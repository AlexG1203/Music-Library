import React from 'react';
import Nav from "../src/components/nav/Nav";
import Main from "../src/components/main/Main"

import './global.scss';

const App: React.FC = () => {
  return (
      <div className="outerWrap">
        <div className="App">
          <Nav/>
          <Main/>
        </div>
      </div>
  )
}

export default App;