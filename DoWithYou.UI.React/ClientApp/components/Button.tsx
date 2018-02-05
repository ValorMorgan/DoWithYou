import * as React from 'react';
import { ICommonProps } from './Utilities';

export class Button extends React.PureComponent<ICommonProps, {}> {
    constructor(props: ICommonProps) {
        super(props);
    }

    render() {
        return (
            <button {...this.props} className={`button ${this.props.className ? this.props.className : ''}`.trim()}>{this.props.children}</button>
        );
    }
}

export class LoginButton extends React.PureComponent<ICommonProps, {}> {
    constructor(props: ICommonProps) {
        super(props);
    }

    render() {
        return (
            <Button {...this.props} id={`button-login ${this.props.id ? this.props.id : ''}`.trim()}>Log In</Button>
        );
    }
}

export class RegisterButton extends React.PureComponent<ICommonProps, {}> {
    constructor(props: ICommonProps) {
        super(props);
    }

    render() {
        return (
            <Button {...this.props} id={`button-register ${this.props.id ? this.props.id : ''}`.trim()}>Register</Button>
        );
    }
}