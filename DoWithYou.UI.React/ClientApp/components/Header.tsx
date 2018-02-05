import * as React from 'react';
import * as Utilities from './Utilities';
import { LoginButton, RegisterButton } from './Button';

export class Header extends React.Component<{}, {}> {
    public render() {
        return (
            <div id='header'>
                <LoginButton />
                <RegisterButton />
                <Utilities.ClearFix />
            </div>
        );
    }
}