import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from '../Navbar/NavMenu';

export class Layout extends React.Component<{children : any}, {}> {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
