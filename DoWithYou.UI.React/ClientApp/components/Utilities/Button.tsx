import * as React from 'react';
import { ICommonProps } from './Misc';

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
            <Button {...this.props} className={`button-login ${this.props.className ? this.props.className : ''}`.trim()}>Log In</Button>
        );
    }
}

export class RegisterButton extends React.PureComponent<ICommonProps, {}> {
    constructor(props: ICommonProps) {
        super(props);
    }

    render() {
        return (
            <Button {...this.props} className={`button-register ${this.props.className ? this.props.className : ''}`.trim()}>Register</Button>
        );
    }
}