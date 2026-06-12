export interface AvatarProps {
  /** Full name — initials are derived when no image is given. */
  name?: string;
  /** Optional image URL. */
  src?: string | null;
  shape?: 'rounded' | 'circle';
  size?: 'sm' | 'md' | 'lg';
}

/** User/company avatar. Rounded-square by default (entities); circle for people. */
export function Avatar(props: AvatarProps): JSX.Element;
