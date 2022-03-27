import React, { Component } from 'react';
import Chat from '../../src/Chat/Chat'

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div style={{ margin: '0 30%' }}>
            <Chat />
        </div>
    );
  }
}
