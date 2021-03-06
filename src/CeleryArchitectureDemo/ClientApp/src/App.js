import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./features/home/Home";
import { Weather } from "./features/weather/Weather";
import { CounterPage } from "./features/counter/CounterPage";
import { Todo } from "./features/todo/Todo";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/counter" component={CounterPage} />
        <Route path="/weather" component={Weather} />
        <Route path="/todo" component={Todo} />
      </Layout>
    );
  }
}
