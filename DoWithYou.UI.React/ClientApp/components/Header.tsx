import * as React from 'react';
import { LoginButton, RegisterButton } from './Utilities/Button';

export class Header extends React.Component<{}, {}> {
    render() {
        return (
            <div id='header'>
                <LoginButton />
                <RegisterButton />
            </div>
        );
    }
}