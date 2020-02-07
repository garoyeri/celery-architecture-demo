import React, { Component } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";

export class AddItem extends Component {
    constructor(props) {
        super(props);

        this.state = { description: "", adding: false }

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ description: event.target.value });
    }

    handleSubmit(event) {
        this.setState({ adding: true });
        fetch('/api/Todo',
            {
                method: 'POST',
                body: JSON.stringify({
                    description: this.state.description
                }),
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                }
            }).then(response => {
            this.setState({ description: "" });
            this.props.onRequireReload();
        });
        event.preventDefault();
    }

    render() {
        return (
                        <
            div >
             <
            Form >
             <
            FormGroup >
             <
            Label
        for=
        "description" > Description < /
        Label >  < Input
        type = "text"
        name = "description"
        id = "description"
        placeholder = "Enter new TODO item text"
        value = { this.state.description }
        onChange = { this.handleChange } /  >  < /
        FormGroup >  < Button
        color = "primary"
        onClick = { this.handleSubmit } > Add < /
        Button >  < /
        Form >  < /
        div > 
    );
}
}