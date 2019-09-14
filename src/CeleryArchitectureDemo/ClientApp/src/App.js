import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './features/home/Home';
import { Weather } from './features/weather/Weather';
import { Counter } from './features/counter/Counter';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/weather' component={Weather} />
      </Layout>
    );
  }
}
