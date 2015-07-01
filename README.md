# MaskedTextBoxBehavior
This is a XAML behavior for Windows 8.1 Store Apps to include a mask for text input on a TextBox.  This is just an initial version so any and all contributions, critiques and complaints are welcome!

## Adding to your project
The easiest way is to clone or download this repository and add the MaskedTextBoxBehavior.csproj to your solution.  Otherwise, you can add just the MaskedTextBoxBehavior.cs to an existing Windows 8.1 Store Application directly.

You must also add a reference to the Behavior SDK extension in order to include the appropriate XAML markup:

1. Start adding a new reference to your Windows 8.1 Store App
2. Select Windows 8.1 and then Extensions
3. Ensure the Behaviors SDK is selected

Next, ensure you have included the XML namespace using statements in your xaml file:

1. Add a declaration for the Interactivity namespace to your root/Page node

    *xmlns:i="using:Microsoft.Xaml.Interactivity"*

2. Add a declaration for the behavior namespace to your root/Page node
  
    *xmlns:mask="using:MaskedTextBoxBehavior"*

Your XAML file should look similar to this:

    <Page
        x:Class="MaskedTextBoxBehavior.Win8.MainPage"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="using:MaskedTextBoxBehavior.Win8"
        xmlns:mask="using:MaskedTextBoxBehavior"
        xmlns:i="using:Microsoft.Xaml.Interactivity"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d">

## Usage
This behavior is rather easy to use and can be added to any TextBox control. Refer to the sample project included in this repository for syntax.

First, add the behavior to a given TextBox:
        <TextBox>
            <i:Interaction.Behaviors>
                <mask:MaskedTextBoxBehavior Pattern="AAA-AA-AAAA">
                    
                </mask:MaskedTextBoxBehavior>
            </i:Interaction.Behaviors>
        </TextBox>

Next, choose any combination of the symbols below or other characters:

* # - Represents a numeric-only digit
* A - Represents an alphanumeric digit

Any other character will be treated like a literal.

## Examples

_SSN - Social Security Number_

Pattern: ###-##-###

_US Phone Number_

Pattern: (###) ###-####

