import React, { Component } from "react";
import { TodoList } from "./components/TodoList";
import { AddItem } from "./components/AddItem";

export class Todo extends Component {
    static displayName = Todo.name;

    constructor(props) {
        super(props);
        this.state = { todoItems: [], loading: true };

        this.reload = this.reload.bind(this);

        this.reload();
    }

    reload() {
        fetch("/api/Todo")
            .then(response => response.json())
            .then(data => {
                this.setState({ todoItems: data.todoItems, loading: false });
            });
    }

    static renderComponents(todoItems, self) {
        return (
        <>
        <
        TodoList
        todoItems = { todoItems }
        onRequireReload = { self.reload }></
        TodoList >  < AddItem
        onRequireReload = { self.reload }></
        AddItem >  < />
    );
}

render()
{
    let contents = this.state.loading
        ? (
                    <
        p >
         <
        em >
        Loading...</
    em >  < /
    p > 
) :
(
    Todo.renderComponents(this.state.todoItems, this)
);

return (
    <
    div >
    <
    h1 >
    Todo
List < /
h1 >  < p > Keep
track
of
what
you
're doing, really simply.</p>
{
    contents
}
</
div > 
);
}
}