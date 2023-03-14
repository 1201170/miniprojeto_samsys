import React from 'react';

export class Home extends React.Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Mini Projeto Samsys</h1>
        <br></br>
        <h3>This is the main home page</h3>
        <p>You can use the navbar to check the list of books</p>
      </div>
    );
  }
}
