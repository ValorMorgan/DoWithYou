import * as React from 'react'

export class ClearFix extends React.PureComponent<{}, {}> {
    public render() {
        return (
            <div className="clearFix"></div>
        );
    }
}

export class Icon extends React.PureComponent<{icon: string}, {}> {
    public render() {
        const className = `glyphicon ${this.props.icon}`;

        return (
            <span className={className}></span>
        );
    }
}