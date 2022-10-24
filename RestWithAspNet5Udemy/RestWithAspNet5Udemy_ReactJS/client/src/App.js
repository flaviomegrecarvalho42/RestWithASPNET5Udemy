//#region Passando propriedades para o HTML
/*1a maneira
import React from 'react';
import Header from './Header';

export default function App() {
  return (
    <Header title="Client REST Udemy - Properties" />
  );
}
*/

/*2a maneira
import React from 'react';
import Header from './Header';

export default function App() {
  return (
    <Header>
      Client REST Udemy - Properties
    </Header>
  );
}
*/
//#endregion

//#region Implementando o conceito de estado e imutabilidade
/*
import React, { useState } from 'react';
import Header from './Header';

export default function App() {
  const [counter, setCounter] = useState(0);

  function increment() {
    setCounter(counter + 1);
  }

  return (
    <div>
      <Header>
        Counter: {counter}
      </Header>
      <button onClick={increment}>Add</button>
    </div>
  );
}
*/
//#endregion

import React from 'react';
import './global.css';
import Routes from './routes'

export default function App() {
  return (
    <Routes />
  );
}

