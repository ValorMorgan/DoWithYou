import * as React from 'react'

export interface ICommonProps {
    id?: string;
    className?: string;
    onClick?: (string | any);
}

export const ClearFix = () =>
    <div className="clearFix"></div>;

export const Icon = (icon: string) =>
    <span className={`glyphicon ${icon}`.trim()}></span>;