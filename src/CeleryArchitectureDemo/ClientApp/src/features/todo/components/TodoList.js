import React, { Component } from "react";
import { Button } from "reactstrap";

export class TodoList extends Component {
  constructor(props) {
    super(props);

    this.handleComplete = this.handleComplete.bind(this);
  }

  handleComplete(id) {
    fetch(`/api/Todo/${id}/Complete`, {
      method: "POST"
    }).then(response => {
      this.props.onRequireReload();
    });
  }

  render() {
    return (
      <table className="table table-striped">
        <thead>
          <tr>
            <th> ID </th> <th> Description </th> <th> When Completed </th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {this.props.todoItems.map(item => (
            <tr key={item.id}>
              <td>{item.id} </td> <td> {item.description} </td>
              <td> {item.whenCompleted} </td>
              <td>
                <Button
                  color="success"
                  disabled={item.isCompleted}
                  onClick={() => this.handleComplete(item.id)}
                >
                  Complete
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }
}
