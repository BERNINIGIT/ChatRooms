import React, { Component } from 'react';
import authService from '../../../src/components/api-authorization/AuthorizeService';

export class ChatInput extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            message : ''
        }
    }
    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated] = await Promise.all([authService.isAuthenticated()]);
        this.setState({
            isAuthenticated
        });
    }

    

    onSubmit = (e) => {
        e.preventDefault();

        const isMessageProvided = this.state.message && this.state.message !== '';

        if (isMessageProvided) {
            this.props.sendMessage(this.state.message);
        }
        else {
            alert('Please provide a message to send.');
        }
    }

    onMessageUpdate = (e) => {
        //setMessage(e.target.value);
        this.setState({
            message: e.target.value
        });
    }
    render() {
        const { isAuthenticated} = this.state;
        if (!isAuthenticated) {
            return this.anonymousView();
        } else {
            return this.authenticatedView();
        }
    }

    authenticatedView() {
        return (
            <form
                onSubmit={this.onSubmit}>
                <label htmlFor="message">Message:</label>
                <br />
                <input
                    type="text"
                    id="message"
                    name="message"
                    value={this.state.message}
                    onChange={this.onMessageUpdate} />
                <br /><br />
                <button>Submit</button>
            </form>
            );
    }

    anonymousView() {
        return (<span>Please log in to send messages</span>);
    }
}